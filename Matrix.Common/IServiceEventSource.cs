using System;
using System.Diagnostics.Tracing;
using System.Fabric;

namespace Matrix.Common
{
    public interface IServiceEventSource
    {
        void Message(string message, params object[] args);
        void Message(string message);
        void ServiceMessage(ServiceContext serviceContext, string message, params object[] args);
        void ServiceTypeRegistered(int hostProcessId, string serviceType);
        void ServiceHostInitializationFailed(string exception);
        void ServiceRequestStart(string requestTypeName);
        void ServiceRequestStop(string requestTypeName, string exception = "");
        bool IsEnabled();
        bool IsEnabled(EventLevel level, EventKeywords keywords);
        string ToString();
        void Dispose();
        string Name { get; }
        Guid Guid { get; }
        Exception ConstructionException { get; }
    }
}