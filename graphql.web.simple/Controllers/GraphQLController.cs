using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Graphql.Web.Schema;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation.Complexity;

namespace Graphql.Web.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GraphQLController : ApiController
    {
        private readonly ISchema _schema;
        private readonly DataLoaderDocumentListener _dataLoaderListener;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        public GraphQLController()
        {
            _executer = new DocumentExecuter();
            _writer = new DocumentWriter();
            _schema = new TestSchema();
            _dataLoaderListener = new DataLoaderDocumentListener(new DataLoaderContextAccessor());
        }

        [HttpPost]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(HttpRequestMessage request, GraphQLQuery query)
        {
            if (request.Method == HttpMethod.Get)
            {
                Request.RequestUri.TryReadQueryAs(out query);
            }

            var httpResult = HttpStatusCode.OK;
            var json = string.Empty;
            if (query != null)
            {
                var input = query.Variables?.ToString().ToInputs();

                var result = await _executer.ExecuteAsync(_ =>
                {
                    _.Schema = _schema;
                    _.Query = query.Query;
                    _.OperationName = query.OperationName;
                    _.Inputs = input;
                    _.ComplexityConfiguration = new ComplexityConfiguration {MaxDepth = 250};
                    _.FieldMiddleware.Use<ErrorLogFieldsMiddleware>();
                    _.Listeners.Add(_dataLoaderListener);

                }).ConfigureAwait(false);

                if (result != null && result.Errors != null && result.Errors.Any())
                {
                    httpResult = HttpStatusCode.BadRequest;
                    foreach (var error in result.Errors)
                    {
                        //_log.Exception("GraphQL: " + JsonConvert.SerializeObject(error));
                    }
                    json = _writer.Write(result.Errors.First());
                    var errorResponse = request.CreateResponse(httpResult);
                    errorResponse.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return errorResponse;
                    
                }
                else
                {
                    httpResult = HttpStatusCode.OK;
                }

                json = _writer.Write(result);
            }

            var response = request.CreateResponse(httpResult);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return response;
        }
    }
}