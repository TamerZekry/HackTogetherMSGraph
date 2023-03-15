using HackTogetherMSGraph.Models;
using HackTogetherMSGraph.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace HackTogetherMSGraph.Controllers
{
    public class MainPageController : Controller
    {
        private readonly UserProfile _userProfile;
        private readonly UserEmails _userEmails;
        private readonly MainPageData _pageData;
        private readonly UsersProfile _usersProfile;
        private readonly Groups _groups;

        public MainPageController(UserProfile userProfile, UserEmails userEmails, MainPageData pageData,UsersProfile usersProfile, Groups groups)
        {
            _userProfile = userProfile;
            _userEmails = userEmails;
            _pageData = pageData;
            _usersProfile = usersProfile;
            _groups = groups;
        }
        public async Task<IActionResult> IndexAsync()
        {
            User currentUser = await _userProfile.GetUserProfile();
            _pageData.UserName = currentUser.DisplayName;
            _pageData.EmailsCount = _userEmails.GetEmailsCount().Result;
            _pageData.UsersCount = _usersProfile.GetUsersCount().Result;
            _pageData.GroupsCount = _groups.GetGroupCount().Result; 


            return View(_pageData);
        }
    }
}
