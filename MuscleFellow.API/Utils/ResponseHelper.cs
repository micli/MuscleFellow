using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuscleFellow.API.Utils
{
    public class ResponseHelper
    {
        public static StatusCodeResult Created()
        {
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
        public static StatusCodeResult Accepted()
        {
            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }
        public static StatusCodeResult NonAuthoritative()
        {
            return new StatusCodeResult(StatusCodes.Status203NonAuthoritative);
        }
        public static StatusCodeResult NoContent()
        {
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
        public static StatusCodeResult ResetContent()
        {
            return new StatusCodeResult(StatusCodes.Status205ResetContent);
        }
        public static StatusCodeResult PartialContent()
        {
            return new StatusCodeResult(StatusCodes.Status206PartialContent);
        }
        public static StatusCodeResult MultipleChoices()
        {
            return new StatusCodeResult(StatusCodes.Status300MultipleChoices);
        }
        public static StatusCodeResult MovedPermanently()
        {
            return new StatusCodeResult(StatusCodes.Status301MovedPermanently);
        }
        public static StatusCodeResult Found()
        {
            return new StatusCodeResult(StatusCodes.Status302Found);
        }
        public static StatusCodeResult SeeOther()
        {
            return new StatusCodeResult(StatusCodes.Status303SeeOther);
        }
        public static StatusCodeResult NotModified()
        {
            return new StatusCodeResult(StatusCodes.Status304NotModified);
        }
        public static StatusCodeResult UseProxy()
        {
            return new StatusCodeResult(StatusCodes.Status305UseProxy);
        }
        public static StatusCodeResult SwitchProxy()
        {
            return new StatusCodeResult(StatusCodes.Status306SwitchProxy);
        }
        public static StatusCodeResult TemporaryRedirect()
        {
            return new StatusCodeResult(StatusCodes.Status307TemporaryRedirect);
        }
        public static StatusCodeResult BadRequest()
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
        public static StatusCodeResult Unauthorized()
        {
            return new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
        public static StatusCodeResult PaymentRequired()
        {
            return new StatusCodeResult(StatusCodes.Status402PaymentRequired);
        }
        public static StatusCodeResult Forbidden()
        {
            return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
        public static StatusCodeResult NotFound()
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }
        public static StatusCodeResult MethodNotAllowed()
        {
            return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
        }
        public static StatusCodeResult NotAcceptable()
        {
            return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
        }
        public static StatusCodeResult ProxyAuthenticationRequired()
        {
            return new StatusCodeResult(StatusCodes.Status407ProxyAuthenticationRequired);
        }
        public static StatusCodeResult RequestTimeout()
        {
            return new StatusCodeResult(StatusCodes.Status408RequestTimeout);
        }
        public static StatusCodeResult Conflict()
        {
            return new StatusCodeResult(StatusCodes.Status409Conflict);
        }
        public static StatusCodeResult Gone()
        {
            return new StatusCodeResult(StatusCodes.Status410Gone);
        }
        public static StatusCodeResult LengthRequired()
        {
            return new StatusCodeResult(StatusCodes.Status411LengthRequired);
        }
        public static StatusCodeResult PreconditionFailed()
        {
            return new StatusCodeResult(StatusCodes.Status412PreconditionFailed);
        }
        public static StatusCodeResult RequestEntityTooLarge()
        {
            return new StatusCodeResult(StatusCodes.Status413RequestEntityTooLarge);
        }
        public static StatusCodeResult RequestUriTooLong()
        {
            return new StatusCodeResult(StatusCodes.Status414RequestUriTooLong);
        }
        public static StatusCodeResult UnsupportedMediaType()
        {
            return new StatusCodeResult(StatusCodes.Status415UnsupportedMediaType);
        }
        public static StatusCodeResult RequestedRangeNotSatisfiable()
        {
            return new StatusCodeResult(StatusCodes.Status416RequestedRangeNotSatisfiable);
        }
        public static StatusCodeResult ExpectationFailed()
        {
            return new StatusCodeResult(StatusCodes.Status417ExpectationFailed);
        }
        public static StatusCodeResult ImATeapot()
        {
            return new StatusCodeResult(StatusCodes.Status418ImATeapot);
        }
        public static StatusCodeResult AuthenticationTimeout()
        {
            return new StatusCodeResult(StatusCodes.Status419AuthenticationTimeout);
        }
        public static StatusCodeResult InternalServerError()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
        public static StatusCodeResult NotImplemented()
        {
            return new StatusCodeResult(StatusCodes.Status501NotImplemented);
        }
        public static StatusCodeResult BadGateway()
        {
            return new StatusCodeResult(StatusCodes.Status502BadGateway);
        }
        public static StatusCodeResult ServiceUnavailable()
        {
            return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
        }
        public static StatusCodeResult GatewayTimeout()
        {
            return new StatusCodeResult(StatusCodes.Status504GatewayTimeout);
        }
        public static StatusCodeResult HttpVersionNotsupported()
        {
            return new StatusCodeResult(StatusCodes.Status505HttpVersionNotsupported);
        }
        public static StatusCodeResult VariantAlsoNegotiates()
        {
            return new StatusCodeResult(StatusCodes.Status506VariantAlsoNegotiates);
        }
    }
}
