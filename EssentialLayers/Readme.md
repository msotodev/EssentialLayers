# Essential Layers

Essential layers is a layer library for starting projects from scratch with a robust structure through the management of results, cache, error handling, helpers and extensions.

It can be used for multiple projects in the **.NET** environment, that is, mobile applications with **NET Maui** or **Xamarin Forms**, web applications with **Blazor** or **ASP NET MVC** and API projects with **ASP NET**

#### Helpers

| Helper | Description |
| :- | :- |
| [Result](/EssentialLayers/Helpers/Result) | It is used for error handling, results and exception control. |
| [Cache](/EssentialLayers/Helpers/Cache) | The quickest way to implement cache. |
| [Extension](EssentialLayers/Helpers/Extension) | List of methods clasified by data type to extends the functionality and make language more readable. |
| [Logger](/EssentialLayers/Helpers/Logger) | manage the essential methods at the app logger. |

#### Release Notes
 - Updating of dependencies to the last version `14-10-2025`
 - New ToBase64 extension in String extension `14/08/2025`
 - New Response design pattern to manage responses of methods and override to Success in ResultHelper `29/07/2025`
 - Better implementation of RemoveDiacritics; New parameter 'includeWhiteSpaces' in IsEmpty and NotEmpty in String extensions `21-05-2025`
 - New TryHanlder and fixed issues in Capitalize extension `07/04/2025`
 - Null validation in String extensions `03/03/2025`
 - Serializer has changed from System.Text.Json to Newtonsoft.Json `18/02/2025`
 - Updating of nuget packages to the last version `18/02/2025`
 - New compress methods to bytes array and streams `13/01/2025`

Created by [Mario Soto Moreno](https://github.com/MatProgrammerSM)