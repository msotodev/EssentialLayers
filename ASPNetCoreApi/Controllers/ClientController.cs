using EssentialLayers.Dapper.Interfaces;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientController(
		IQueryService queryService,
		INormalProcedure normalProcedure
	) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			ResultHelper<IEnumerable<ClientDto>> result = await normalProcedure.ExecuteAllAsync<ClientDto>(
				"SpGetClient"
			);

			if (result.Ok.False()) return BadRequest(result.Message);

			return Ok(result.Data.ToList());
		}

		[HttpGet("{idClient}")]
		public async Task<IActionResult> GetAsync(int idClient)
		{
			ResultHelper<ClientConfigurationDto> result = await queryService.QueryFirstAsync<ClientConfigurationDto>(
				$"""
					SELECT C.IdClient,
						C.Name,
						C.Active,
						CC.ApplicationURL,
						CC.SASToken,
						CC.StorageAccount,
						CC.FinePhotoContainer,
						CC.SenderID,
						CC.ListenConnectionString,
						CC.NotificationHubName,
						CC.Description,
						CC.EmailURLFine,
						CC.EmailURLPayment,
						CC.UriLicensePlate,
						CC.UriLicense,
						CC.UriCustomer,
						CC.UriTheftReport,
						CC.UriPendingFinesCount,
						CC.UriPayment,
						CC.UriPaymentReference,
						CC.PublicKey,
						CC.TestPublicKey,
						CC.OfficerGroup,
						CC.MerchantId,
						CC.TokenConfiguration,
						CC.TimeZoneOffset,
						CC.UpdateUrl,
						CC.PrintHeader
					FROM dbo.Client C
						INNER JOIN ClientConfiguration CC ON CC.IdClient = C.IdClient
					WHERE C.IdClient = @IdClient;
				""", new
				{
					IdClient = idClient
				}
			);

			if (result.Ok.False()) return BadRequest(result.Message);

			return Ok(result.Data);
		}

		public record ClientDto(bool Success, string Message, int IdClient, string Name, bool Active);

		public record ClientConfigurationDto(
			int IdClient,
			string Name,
			bool Active,
			string ApplicationURL,
			string SASToken,
			string StorageAccount,
			string FinePhotoContainer,
			string SenderID,
			string ListenConnectionString,
			string NotificationHubName,
			string Description,
			string EmailURLFine,
			string EmailURLPayment,
			string UriLicensePlate,
			string UriLicense,
			string UriCustomer,
			string UriTheftReport,
			string UriPendingFinesCount,
			string UriPayment,
			string UriPaymentReference,
			string PublicKey,
			string TestPublicKey,
			string OfficerGroup,
			string MerchantId,
			string TokenConfiguration,
			short TimeZoneOffset,
			string UpdateUrl,
			string PrintHeader
		);
	}
}