using Newtonsoft.Json;
using Remita.Models.Utility;
using System.Net;

namespace Remita.Api.Tests.Integration.Helpers;
internal static class ResponseAssertionUtility
{
    public static async Task AssertResponse<T>(HttpResponseMessage responseMessage, HttpStatusCode expetedStatusCode, ApiRecordResponse<T> expectedResponse) where T : BaseRecord
    {
        string content = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ApiRecordResponse<T>>(content);

        Assert.Equal(expetedStatusCode, responseMessage.StatusCode);
        Assert.Equal(expectedResponse, response);
    }

    public static async Task AssertResponse<T>(HttpResponseMessage responseMessage, HttpStatusCode expetedStatusCode, ApiResponse<T> expectedResponse) where T : class
    {
        string content = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ApiResponse<T>>(content);

        Assert.Equal(expetedStatusCode, responseMessage.StatusCode);
        Assert.Equal(expectedResponse, response);
    }

    public static async Task AssertPaginatedResponse<T>(HttpResponseMessage responseMessage, HttpStatusCode expetedStatusCode, ApiRecordResponse<PaginationResponse<T>> expectedResponse) where T : BaseRecord
    {
        string content = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ApiRecordResponse<PaginationResponse<T>>>(content);

        Assert.NotNull(response);

        Assert.Equal(expetedStatusCode, responseMessage.StatusCode);
        Assert.Equal(expectedResponse.IsSuccessful, response.IsSuccessful);
        Assert.Equal(expectedResponse.Message, response.Message);

        if (response.Data == null || expectedResponse.Data == null)
        {
            Assert.Equal(expectedResponse.Data, response.Data);
            return;
        }

        Assert.Equal(expectedResponse.Data.CurrentPage, response.Data.CurrentPage);
        Assert.Equal(expectedResponse.Data.TotalPages, response.Data.TotalPages);
        Assert.Equal(expectedResponse.Data.PageSize, response.Data.PageSize);
        Assert.Equal(expectedResponse.Data.TotalRecords, response.Data.TotalRecords);

        var records = response.Data.Records.ToList();
        var expectedRecords = expectedResponse.Data.Records.ToList();
        for (int i = 0; i < records.Count; i++)
        {
            var record = records[i];
            var expectedRecord = expectedRecords[i];
            Assert.Equal(expectedRecord, record);
        }
    }
}