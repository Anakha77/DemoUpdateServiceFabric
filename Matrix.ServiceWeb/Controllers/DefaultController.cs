using System;
using System.Fabric;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;

namespace Matrix.ServiceWeb.Controllers
{
    [RoutePrefix("api")]
    public class DefaultController : ApiController
    {
        private static readonly Uri ServiceUri;
        private static readonly FabricClient FabricClient;
        private static readonly HttpCommunicationClientFactory CommunicationFactory;

        static DefaultController()
        {
            ServiceUri = new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/MatrixService");
            FabricClient = new FabricClient();

            CommunicationFactory = new HttpCommunicationClientFactory(new ServicePartitionResolver(() => FabricClient));
        }

        [HttpGet]
        [Route("GetMatrix")]
        public async Task<HttpResponseMessage> GetMatrix()
        {
            var matrix = string.Empty;
            var partitionClient = new ServicePartitionClient<HttpCommunicationClient>(CommunicationFactory, ServiceUri);

            await
                partitionClient.InvokeWithRetryAsync(
                    async (client) => { matrix = await client.HttpClient.GetStringAsync(new Uri(client.Url, "get")); });

            return new HttpResponseMessage
            {
                Content = new StringContent(matrix)
            };
        }
    }
}
