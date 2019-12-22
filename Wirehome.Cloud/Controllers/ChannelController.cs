﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Wirehome.Cloud.Controllers.Models;
using Wirehome.Cloud.Services.Repository;

namespace Wirehome.Cloud.Controllers
{
    [Authorize]
    public class ChannelController : Controller
    {
        private readonly RepositoryService _repositoryService;

        public ChannelController(RepositoryService repositoryService)
        {
            _repositoryService = repositoryService ?? throw new System.ArgumentNullException(nameof(repositoryService));
        }

        [Route("cloud/channel/deviceNotConnected")]
        [HttpGet]
        public IActionResult DeviceNotConnected()
        {
            return View(nameof(DeviceNotConnected));
        }

        [Route("cloud/channel")]
        [Route("cloud/channel/index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //HttpContext.Response.Cookies.Append(CloudCookieNames.ChannelUid, "default");

            var identityConfiguration = await _repositoryService.TryGetIdentityConfigurationAsync(HttpContext.User.Identity.Name).ConfigureAwait(false);

            var channelsModel = new ChannelsModel
            {
                Channels = identityConfiguration.Channels.Keys.ToList()
            };

            // This controller allows setting the used channel. 
            // The list of channels is provided as radio buttons.
            // Direct call will open the window always.
            // Other controllers can redirect to this view if required.
            return View(nameof(Index), channelsModel);
        }
    }
}
