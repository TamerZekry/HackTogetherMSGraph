using Microsoft.Graph;

namespace HackTogetherMSGraph.Services
{
    public class UserEmails
    {
        private readonly GraphServiceClient _graphServiceClient;

        public UserEmails(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        /// <summary>
        /// Retrieve the top 10 emails
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Message>> GetUserEmails()
        {
            try
            {
                var emails = await _graphServiceClient.Me.Messages
                            .Request()
                            .Select(msg => new
                            {
                                msg.Subject,
                                msg.BodyPreview,
                                msg.ReceivedDateTime
                            })
                            .OrderBy("receivedDateTime desc")
                            .Top(10)
                            .GetAsync();

                return emails.CurrentPage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving emails  : {ex.Message}");
            }
        }
        /// <summary>
        /// retrieve the next emails, next page  link
        /// </summary>
        /// <param name="nextPageLink"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<Message> Messages, string NextLink)> GetUserMessagesPage(
           string nextPageLink = null, int top = 10)
        {
            IUserMessagesCollectionPage pagedMessages;

            if (nextPageLink == null)
            {
                // Get initial page of messages
                pagedMessages = await _graphServiceClient.Me.Messages
                        .Request()
                        .Select(msg => new
                        {
                            msg.Subject,
                            msg.BodyPreview,
                            msg.ReceivedDateTime
                        })
                        .Top(top)
                        .OrderBy("receivedDateTime desc")
                        .GetAsync();
            }
            else
            {
                // Use the next page request URI value to get the page of messages
                var messagesCollectionRequest =
                    new UserMessagesCollectionRequest(nextPageLink, _graphServiceClient, null);
                pagedMessages = await messagesCollectionRequest.GetAsync();
            }

            return (Messages: pagedMessages, NextLink: GetNextLink(pagedMessages));
        }
        private string GetNextLink(IUserMessagesCollectionPage pagedMessages)
        {
            if (pagedMessages.NextPageRequest != null)
            {
                // Get the URL for the next batch of records
                return pagedMessages.NextPageRequest.GetHttpRequestMessage().RequestUri?.OriginalString;
            }
            return null;
        }


        

        public async Task<int> GetEmailsCount()
        {
            var emailsCount = await  _graphServiceClient.Me.Messages
                       .Request().GetAsync() ;
            return emailsCount.Count;
        }







        //public int GetEmailsCount()
        //{
        //    var emailsCount = _graphServiceClient.Me.Messages
        //               .Request().GetAsync().Result.Count;
        //    return emailsCount;
        //}

    }
}
