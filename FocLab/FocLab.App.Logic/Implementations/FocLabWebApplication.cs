﻿using Croco.Core.Contract.Application;
using Croco.Core.Contract.EventSourcing.Abstractions;
using Croco.WebApplication.Application;
using Microsoft.AspNetCore.StaticFiles;

namespace FocLab.App.Logic.Implementations
{
    public class FocLabWebApplication : CrocoWebApplication
    {
        public FocLabWebApplication(ICrocoApplicationOptions opts, 
            CrocoWebApplicationOptions webOpts, IEventSourcer eventSourcer) 
            : base(opts, webOpts, eventSourcer)
        {
        }

        public static string GetMimeMapping(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

            return contentType ?? "application/octet-stream";
        }

        public static bool IsImage(string fileName)
        {
            return GetMimeMapping(fileName).StartsWith("image/");
        }
    }
}