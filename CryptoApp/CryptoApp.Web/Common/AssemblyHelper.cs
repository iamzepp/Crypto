using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CryptoApp.Web.Common
{
     /// <summary>
    /// Методы расширения и вспомгательные методы для работы со сборками
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Получить все сборки загруженные в память, кроме динавических
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetLoadedAssemblies() =>
            AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic);

        /// <summary>
        /// Получить все публичные классы из загруженных сборок
        /// </summary>
        public static IEnumerable<Type> GetAllExportTypes() => GetLoadedAssemblies().SelectMany(x => x.ExportedTypes);


        /// <summary>
        /// Получить все публичные классы имплементирующие класс/интерфейс типа <paramref name="baseType"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetAllExportTypesInfoBasedOn(Type baseType, bool nonAbstract = true) =>
            GetAllExportTypes()
                .Where(baseType.IsAssignableFrom).Where(x => !nonAbstract || !x.IsAbstract)
                .Select(x => x.GetTypeInfo());


        /// <summary>
        /// Получить все публичные классы имплементирующие класс/интерфейс типа <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetAllExportTypesInfoBasedOn<T>(bool nonAbstract = true) =>
            GetAllExportTypesInfoBasedOn(typeof(T), nonAbstract);


        /// <summary>
        /// Получить все публичные классы имплементирующие класс/интерфейс типа <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllExportTypesBasedOn<T>(bool nonAbstract = true) =>
            GetAllExportTypesInfoBasedOn<T>(nonAbstract);

        /// <summary>
        /// Получить все публичные классы имплементирующие класс/интерфейс типа <paramref name="baseType"/>
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllExportTypesBasedOn(Type baseType, bool nonAbstract = true) =>
            GetAllExportTypesInfoBasedOn(baseType, nonAbstract);

        /// <summary>
        /// Получить все классы имплементирующие класс/интерфейс типа <paramref name="baseType"/>
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="nonAbstract"></param>
        /// <param name="includeNonPublic"></param>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetAllTypesInfoBasedOn(Type baseType, bool nonAbstract = true,
            bool includeNonPublic = false) =>
            GetLoadedAssemblies().SelectMany(x => includeNonPublic ? x.DefinedTypes : x.ExportedTypes)
                .Where(baseType.IsAssignableFrom)
                .Where(x => !nonAbstract || !x.IsAbstract)
                .Select(x => x.GetTypeInfo());

        /// <summary>
        /// Получить все классы имплементирующие класс/интерфейс типа <typeparamref name="T"/>
        /// </summary>
        /// <param name="nonAbstract"></param>
        /// <param name="includeNonPublic"></param>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetAllTypesInfoBasedOn<T>(bool nonAbstract = true,
            bool includeNonPublic = false) =>
            GetAllTypesInfoBasedOn(typeof(T), nonAbstract, includeNonPublic);
    }
}