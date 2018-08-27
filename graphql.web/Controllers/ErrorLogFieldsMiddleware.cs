using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Instrumentation;
using GraphQL.Types;

namespace Graphql.Web.Controllers
{
    public class ErrorLogFieldsMiddleware
    {
        public Task<object> Resolve(ResolveFieldContext context, FieldMiddlewareDelegate next)
        {
            string typeName = context.ParentType.Name;
            string fieldName = context.FieldName;

            Dictionary<string, object> metadata = new Dictionary<string, object>()
            {
                {
                    "typeName",
                    typeName
                },
                {
                    "fieldName",
                    fieldName
                }
            };
            using (context.Metrics.Subject("field", context.FieldName, metadata))
            {
                var result = next(context);
                if (result.Exception != null)
                {
                    LogException(result.Exception, typeName, fieldName);
                }

                return result;

            }
        }

        private void LogException(AggregateException exception, object typeName, object fieldName)
        {
            foreach (var innerException in exception.InnerExceptions)
            {
                //log
            }
        }
    }
}