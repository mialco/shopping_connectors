﻿using System;


namespace mialco.shopping.connector.StoreFront.ImportSvc
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.42000
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------



    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.aspdotnetstorefront.com/", ConfigurationName = "IIpx")]
    public interface IIpx
    {

        [System.ServiceModel.OperationContractAttribute(Action = "://www.aspdotnetstorefront.com/DoItUsernamePwd", ReplyAction = "http://www.aspdotnetstorefront.com/DoItUsernamePwd")]
        string DoItUsernamePwd(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.aspdotnetstorefront.com/DoItUsernamePwd", ReplyAction = "http://www.aspdotnetstorefront.com/DoItUsernamePwd")]
        System.Threading.Tasks.Task<string> DoItUsernamePwdAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.aspdotnetstorefront.com/DoIt", ReplyAction = "http://www.aspdotnetstorefront.com/DoIt")]
        string DoIt(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString);

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.aspdotnetstorefront.com/DoIt", ReplyAction = "http://www.aspdotnetstorefront.com/DoIt")]
        System.Threading.Tasks.Task<string> DoItAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IIpxChannel : IIpx, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IpxClient : System.ServiceModel.ClientBase<IIpx>, IIpx
    {

        public IpxClient()
        {
        }

        public IpxClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public IpxClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public IpxClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public IpxClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public string DoItUsernamePwd(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString)
        {
            return base.Channel.DoItUsernamePwd(AuthenticationEMail, AuthenticationPassword, XmlInputRequestString);
        }

        public System.Threading.Tasks.Task<string> DoItUsernamePwdAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString)
        {
            return base.Channel.DoItUsernamePwdAsync(AuthenticationEMail, AuthenticationPassword, XmlInputRequestString);
        }

        public string DoIt(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString)
        {
            return base.Channel.DoIt(AuthenticationEMail, AuthenticationPassword, XmlInputRequestString);
        }

        public System.Threading.Tasks.Task<string> DoItAsync(string AuthenticationEMail, string AuthenticationPassword, string XmlInputRequestString)
        {
            return base.Channel.DoItAsync(AuthenticationEMail, AuthenticationPassword, XmlInputRequestString);
        }
    }


}