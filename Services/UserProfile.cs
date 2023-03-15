
using Microsoft.Graph;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Net;

namespace HackTogetherMSGraph.Services
{
    public class UserProfile
    {
        private readonly GraphServiceClient _graphServiceClient;

        public UserProfile(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }
        /// <summary>
        /// Get the complete user Profile or the Display Name only
        /// </summary>
        /// <param name="DisplayName_Only"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public  async Task<User> GetUserProfile(bool DisplayName_Only=true)
        {
            User currentUser = null;
            try
            {
                if (DisplayName_Only)
                {
                    currentUser = await _graphServiceClient
                        .Me.Request().Select(u => new{u.DisplayName}).GetAsync();
                }
                else
                {
                    currentUser = await _graphServiceClient.Me.Request().GetAsync();
                }
                return currentUser;
            }
             
            catch (ServiceException ex) when (ex.Message.Contains("Continuous access evaluation resulted in claims challenge"))
            {
                throw new Exception($"Microsoft Graph encountered an error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Get the complete user Profile or the  Display Name only
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public  async Task<User> GetUserPhoto()
        {
            User currentUser = null;
            try
            {
                    currentUser = await _graphServiceClient
                        .Me.Request().Select(u => new { u.Photo}).GetAsync();
                return currentUser;
            }

            catch (ServiceException ex) when (ex.Message.Contains("Continuous access evaluation resulted in claims challenge"))
            {
                throw new Exception($"Microsoft Graph encountered an error:{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

    }
}
