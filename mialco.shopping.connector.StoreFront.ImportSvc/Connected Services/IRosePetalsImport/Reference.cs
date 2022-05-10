﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace IRosePetalsImport
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.aspdotnetstorefront.com/", ConfigurationName="IRosePetalsImport.IIpx")]
    public interface IIpx
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.aspdotnetstorefront.com/DoItUsernamePwd", ReplyAction="http://www.aspdotnetstorefront.com/DoItUsernamePwd")]
        System.Threading.Tasks.Task<string> DoItUsernamePwdAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.aspdotnetstorefront.com/DoIt", ReplyAction="http://www.aspdotnetstorefront.com/DoIt")]
        System.Threading.Tasks.Task<string> DoItAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IIpxChannel : IRosePetalsImport.IIpx, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class IpxClient : System.ServiceModel.ClientBase<IRosePetalsImport.IIpx>, IRosePetalsImport.IIpx
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public IpxClient(EndpointConfiguration endpointConfiguration) : 
                base(IpxClient.GetBindingForEndpoint(endpointConfiguration), IpxClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IpxClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(IpxClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IpxClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(IpxClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IpxClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }

		public IpxClient()
		{
		}

		public System.Threading.Tasks.Task<string> DoItUsernamePwdAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString)
        {
            return base.Channel.DoItUsernamePwdAsync(AuthenticationEMail, AuthenticationPassword, XmlInputRequestString);
        }
        
        public System.Threading.Tasks.Task<string> DoItAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString)
        {
            return base.Channel.DoItAsync(AuthenticationEMail, AuthenticationPassword, XmlInputRequestString);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IIpx))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpsBinding_IIpx))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.MetadataExchangeHttpBinding_IIpx))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IIpx))
            {
                return new System.ServiceModel.EndpointAddress("http://irosepetals.com/Ipx.svc");
            }
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpsBinding_IIpx))
            {
                return new System.ServiceModel.EndpointAddress("https://www.irosepetals.com/Ipx.svc");
            }
            if ((endpointConfiguration == EndpointConfiguration.MetadataExchangeHttpBinding_IIpx))
            {
                return new System.ServiceModel.EndpointAddress("http://irosepetals.com/Ipx.svc/mex");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

		public void Close()
		{
			throw new NotImplementedException();
		}

		public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IIpx,
            
            BasicHttpsBinding_IIpx,
            
            MetadataExchangeHttpBinding_IIpx,
        }
    }
}