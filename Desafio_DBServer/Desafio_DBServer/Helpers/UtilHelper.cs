using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Desafio_DBServer.Helpers
{
    public static class UtilHelper
    {
        public static bool IsNull(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static T CloneObject<T>(this T sourceObject) where T : class
        {
            try
            {
                if (sourceObject != null)
                {
                    // Tipo
                    Type type = sourceObject.GetType();

                    // Cria instância do objeto de destino
                    T targetObject = (T)Activator.CreateInstance(type);

                    // Lista todas as propriedades.
                    var properties = type.GetRuntimeProperties();

                    // Copia todos os dados
                    foreach (PropertyInfo proprertie in properties)
                    {
                        if (proprertie.CanWrite)
                        {
                            if (proprertie.PropertyType.ToString().Contains("Collections.Generic.List"))
                            {
                                var oldList = (IList)proprertie.GetValue(sourceObject, null);

                                if (oldList != null)
                                {
                                    Type typeOfList = proprertie.GetMethod.ReturnType.GenericTypeArguments[0];
                                    Type genericListType = typeof(List<>).MakeGenericType(typeOfList);
                                    var newList = (IList)Activator.CreateInstance(genericListType);

                                    foreach (var item in oldList)
                                    {
                                        newList.Add(item.CloneObject());
                                    }
                                    proprertie.SetValue(targetObject, newList, null);
                                }
                                else
                                {
                                    proprertie.SetValue(targetObject, null, null);
                                }
                            }
                            else
                            {
                                proprertie.SetValue(targetObject, proprertie.GetValue(sourceObject, null), null);
                            }
                        }
                    }

                    return targetObject;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FirstCharToUpper(this string input)
        {
            if (input.IsNull())
                return "";

            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
