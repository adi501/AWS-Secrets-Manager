using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS_Secrets_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsManagerController : ControllerBase
    {
        // Ref (to to config aws CLI) : https://k21academy.com/amazon-web-services/aws-solutions-architect/aws-cli/
        // Ref (to secrets manager) : https://codewithmukesh.com/blog/secrets-in-aspnet-core-with-aws-secrets-manager/


        IAmazonSecretsManager _secrets;
        public SecretsManagerController(IAmazonSecretsManager secrets)
        {
            _secrets = secrets;
        }

        [HttpGet]
        [Route("GetSecretValue")]
        public async Task<IActionResult> GetSecretValue()
        {
            var request = new GetSecretValueRequest()
            {
                SecretId = "dev/myapp/connection"  //dev/myapp/connection  : it is the SecretId_Name in aws
            };
            var data = await _secrets.GetSecretValueAsync(request);

            return Ok(data.SecretString);
        }
        [HttpGet]
        [Route("GetPreviousVersionSecretValue")]
        public async Task<IActionResult> GetPreviousVersionSecretValue()
        {
            var request = new GetSecretValueRequest()
            {
                SecretId = "dev/myapp/connection",
                VersionStage = "AWSPREVIOUS"  // to get Previous data after update
               // VersionStage = "AWSCURRENT"  // to get correct data
            };
            var data = await _secrets.GetSecretValueAsync(request);

            return Ok(data.SecretString);
        }
    }
}
