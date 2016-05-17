using FloorballDataManager;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class FloorballRESTClient : RestClient
    {
        private RestRequest request;

        public FloorballRESTClient(string url) : base(url)
        {
            AddHandler("application/json", NewtonsoftJsonSerializer.Default);
            AddHandler("text/json", NewtonsoftJsonSerializer.Default);
            AddHandler("text/x-json", NewtonsoftJsonSerializer.Default);
            AddHandler("text/javascript", NewtonsoftJsonSerializer.Default);
            AddHandler("*+json", NewtonsoftJsonSerializer.Default);
        }

        public IRestResponse ExecuteRequest(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string,string> headers = null)
        {
            RestResponse response;

            request = new RestRequest(path,method);
            if (urlParams != null)
            {
                foreach (var urlParam in urlParams)
                {
                    request.AddUrlSegment(urlParam.Key, urlParam.Value);
                }
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            switch (method)
            {
                case Method.GET:
                    response = ExecuteGET(urlParams) as RestResponse;
                    break;
                case Method.POST:
                    request.RequestFormat = DataFormat.Json;
                    request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                    response = ExecutePOST(body) as RestResponse;
                    break;
                case Method.PUT:
                    response = ExecutePUT() as RestResponse;
                    break;
                case Method.DELETE:
                    response = ExecuteDELETE() as RestResponse;
                    break;
                default:
                    return null;
            }

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            else
            {
                if (response.StatusCode.ToString().First() == '4')
                {
                    throw new Exception(response.Content);
                }
            }

            return response;
        }

        private IRestResponse ExecuteGET(Dictionary<string,string> queryParams)
        {
            if (queryParams != null)
            {
                foreach (var queryParam in queryParams)
                {
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
                }
            }
           
            return ExecuteAsGet(request, "GET");
        }

        private IRestResponse ExecutePOST(object body)
        {
            if (body != null)
            {
                //request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }

            return ExecuteAsPost(request, "POST");
        }

        private IRestResponse ExecuteDELETE()
        {
            return Execute(request);
        }

        private IRestResponse ExecutePUT()
        {
            return Execute(request);
        }

    }
}
