using System;
using System.Diagnostics;
using System.Linq;

namespace Yis.Framework.Core.Helper
{
    public static class ReflectionHelper
    {
        public static Type GetCallingNoAbstractOrPrivateMethodType()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame frame = null;
            Type helperCallerType = stackFrames[1].GetMethod().DeclaringType; //1 : Classe demandant le helper

            frame = stackFrames.First((t) => { return ((t.GetMethod().DeclaringType != typeof(ReflectionHelper)) && (t.GetMethod().DeclaringType != helperCallerType) && (!t.GetMethod().DeclaringType.IsAbstract || t.GetMethod().IsPrivate)); });
            return frame.GetMethod().DeclaringType;
        }
    }
}