using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Cli
{
    /// <summary>
    /// Hilfsklasse für Lambda Expressions
    /// </summary>
    public static class ExpressionUtil
    {
        /// <summary>
        /// Liefert aus der Expression eine MemberInfo Instanz, falls die Expression auf ein Property zeigt. Sonst Null.
        /// </summary>
        /// <param name="propertyExpression">Expression</param>
        /// <returns>MemberInfo oder Null</returns>
        public static MemberInfo MemberInfo(Expression propertyExpression)
        {
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        /// <summary>
        /// Liefert den Namen eines Properties inklusive des ganzen Objektgraphs. Bsp: Foo.Bar.MyProperty
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="expression">Expression</param>
        /// <returns>Namen eines Properties inklusive des ganzen Objektgraphs</returns>
        public static string NameWithPath<T>(Expression<Func<T, object>> expression)
        {
            var stack = new Stack<string>();

            MemberExpression me;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expression.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;

                default:
                    me = expression.Body as MemberExpression;
                    break;
            }

            while (me != null)
            {
                stack.Push(me.Member.Name);
                me = me.Expression as MemberExpression;
            }

            return string.Join(".", stack.ToArray());
        }

        /// <summary>
        /// Liefert den Namen eines Properties
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="expression">Expression</param>
        /// <returns>Propertyname</returns>
        public static string Name<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        /// <summary>
        /// Liefert den vollen Namen inklusive Namespace
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="expression">Expression</param>
        /// <returns>Propertyname</returns>
        public static string FullName<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.DeclaringType.FullName + "." + body.Member.Name;
        }

        /// <summary>
        /// Liefert ein Porperty als PropertyInfo Instanz
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="selector">Expression</param>
        /// <returns>PropertyInfo</returns>
        public static PropertyInfo Property<T, TKey>(Expression<Func<T, TKey>> selector)
        {
            MemberExpression exp = null;

            //this line is necessary, because sometimes the expression comes as Convert(originalexpression)
            if (selector.Body is UnaryExpression)
            {
                UnaryExpression UnExp = (UnaryExpression)selector.Body;
                if (UnExp.Operand is MemberExpression)
                {
                    exp = (MemberExpression)UnExp.Operand;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else if (selector.Body is MemberExpression)
            {
                exp = (MemberExpression)selector.Body;
            }
            else
            {
                throw new ArgumentException();
            }

            return (PropertyInfo)exp.Member;
        }
    }


}
