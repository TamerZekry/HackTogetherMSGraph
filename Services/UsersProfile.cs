using Microsoft.Graph;

namespace HackTogetherMSGraph.Services
{
    public class UsersProfile
    {
        private readonly GraphServiceClient _graphServiceClient;

        public UsersProfile(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task<int> GetUsersCount()
        {

            var userCount =  await _graphServiceClient.Users.Request().GetAsync();

            return userCount.Count ;


        }

    }
}
