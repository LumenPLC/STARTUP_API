using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.Entities.Google_Drive;
using static Google.Apis.Drive.v3.DriveService;

namespace Infrastructure.GoogleDriveService
{
    internal class GoogleService
    {
        private GoogleCredential _googleCredential;

        public GoogleService(IConfiguration configuration)
        {
            _googleCredential = new GoogleCredential(configuration);
        }

        public DriveService GetService()
        {
            var googleCredentialVM = _googleCredential.GetGoogleDriveCredential();
            var username = googleCredentialVM.USER_NAME;
            var applicationName = googleCredentialVM.APPLICATION_NAME;
            string clientId = googleCredentialVM.CLIENT_ID;
            string clientSecret = googleCredentialVM.CLIENT_SECRET;
            string refreshToken = googleCredentialVM.REFRESH_TOKEN;
            var accessToken = googleCredentialVM.ACCESS_TOKEN;
            try
            {
                var tokenResponse = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };

                var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    Scopes = new[] { Scope.Drive },
                    DataStore = new FileDataStore(applicationName, true),

                });
                var credential = new UserCredential(apiCodeFlow, username, tokenResponse);

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName
                });
                return service;
            }
            catch (Exception e)
            {
                // Handle exception appropriately
                return null;
            }
        }
    }

}
