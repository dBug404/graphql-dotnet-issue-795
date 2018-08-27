using System.Runtime.Serialization;

namespace Graphql.Web.Controllers
{
    [DataContract]
    public class GraphQLQuery
    {
        [DataMember(Name = "operationName")]
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        [DataMember(Name = "query")]
        public string Query { get; set; }
        [DataMember(Name = "variables")]
        public object Variables { get; set; }
    }
}