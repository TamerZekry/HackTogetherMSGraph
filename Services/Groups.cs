using Microsoft.Graph;

namespace HackTogetherMSGraph.Services
{
    public class Groups
    {
        private readonly GraphServiceClient _graphServiceClient;

        public Groups(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task<int> GetGroupCount()
        {
            var groupCount = _graphServiceClient.Groups.Request().GetAsync();
            return groupCount.Result.Count;
        }
    }
}
