using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HopDongMgr
{
    public class ReflectionController
    {
        public class ReflectionResult
        {
            public string Controller { get; set; }
            public string Action { get; set; }
            public string Attributes { get; set; }
            public string ReturnType { get; set; }
        }

        //Lấy tất cả controller, action, attribute
        public List<ReflectionResult> GetControllerAndAction(string namespaces)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<ReflectionResult> controlleractionlist = assembly.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new ReflectionResult() { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            return controlleractionlist;
        }

        //Lấy danh sách các Controller
        public List<Type> GetController(string namespaces)
        {
            List<Type> listController = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces)).OrderBy(x => x.Name);
            return types.ToList();
        }

        //Lấy danh sách các action theo Controller
        public List<string> GetActions(Type controller)
        {
            List<string> listAction = new List<string>();
            IEnumerable<MemberInfo> memberInfo = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);
            foreach (MemberInfo method in memberInfo)
            {
                Object[] myAttributes = method.GetCustomAttributes(typeof(HttpPostAttribute), true);
                Object[] myAttributes1 = method.GetCustomAttributes(typeof(HttpGetAttribute), true);
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(method.Name.ToString());
                }
            }
            return listAction;
        }
    }
}