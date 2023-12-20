using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace SportsLibrary.Utilities
{
    /// <summary>
    /// Utility class for working with JSON serialization and manipulation.
    /// </summary>
    public static class JsonUtils
    {
        #region Serialize 

        /// <summary>
        /// Serializes an object to a JSON-formatted string with specified formatting options.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A JSON-formatted string representing the serialized object.</returns>
        public static string Serialize<T>(T obj)
        {
            // Configure the serializer settings for indented formatting
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
            return JsonConvert.SerializeObject(obj, settings);
        }

        #endregion

        #region Deserialize

        /// <summary>
        /// Deserializes a JSON-formatted string to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to.</typeparam>
        /// <param name="json">The JSON-formatted string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            // Configure the serializer settings for object creation handling
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            var obj = JsonConvert.DeserializeObject<T>(json, settings);
            if(obj == null) throw new NullReferenceException(nameof(obj));
            return obj;
        }

        #endregion

        #region Search

        #region Find

        /// <summary>
        /// Searches a JSON-formatted string for a specified string value and returns the found value.
        /// </summary>
        /// <param name="jsonString">The JSON-formatted string to search.</param>
        /// <param name="searchValue">The string value to search for.</param>
        /// <returns>The found string value.</returns>
        public static string SearchJsonForStringValue(string jsonString, string searchValue)
        {
            // Initialize the result token to null
            JToken? result = null;

            // Parse the input JSON string into a JToken
            JToken json = JToken.Parse(jsonString);

            // Call the recursive search method
            result = SearchJsonToken(json, searchValue);

            if(result ==  null) throw new NullReferenceException(nameof(result));
            // Return the found string value (or null if not found)
            return result.ToString();
        }

        /// <summary>
        /// Recursive helper method to search a JSON token for a specified string value and return the parent token.
        /// </summary>
        /// <param name="token">The JSON token to search.</param>
        /// <param name="searchValue">The string value to search for.</param>
        /// <returns>The parent token containing the found string value.</returns>
        private static JToken? SearchJsonToken(JToken token, string searchValue)
        {
            // Check if the token is an object
            if (token.Type == JTokenType.Object)
            {
                // Iterate through each property of the object
                foreach (JProperty child in token.Children<JProperty>())
                {
                    // Check if the child property contains the search value
                    if (child.Value.Type == JTokenType.String && child.Value.Value<string>() == searchValue)
                        return child.Parent;

                    // Recursive call to search deeper into the JSON structure
                    JToken? result = SearchJsonToken(child.Value, searchValue);
                    if (result != null)
                        return result;
                }
            }
            // Check if the token is an array
            else if (token.Type == JTokenType.Array)
            {
                // Iterate through each element of the array
                foreach (JToken child in token.Children())
                {
                    // Recursive call to search deeper into the JSON structure
                    JToken? result = SearchJsonToken(child, searchValue);
                    if (result != null)
                        return result;
                }
            }

            // Return null if the search value is not found in the current token
            return null;
        }

        #endregion

        #region Replace

        /// <summary>
        /// Replaces a specified string value in a JSON-formatted string with a new string value.
        /// </summary>
        /// <param name="jsonString">The JSON-formatted string to modify.</param>
        /// <param name="searchValue">The string value to replace.</param>
        /// <param name="replacementValue">The new string value to use as a replacement.</param>
        /// <returns>The modified JSON-formatted string.</returns>
        public static string ReplaceJsonStringValue(string jsonString, string searchValue, string replacementValue)
        {
            // Parse the input JSON string into a JToken
            JToken json = JToken.Parse(jsonString);

            // Parse the replacement value into a JToken
            JToken replacement = JToken.Parse(replacementValue);

            // Find the token to be replaced
            JToken? search = SearchJsonToken(json, searchValue);
            if(search == null) throw new NullReferenceException(nameof(search));
            // Replace the token
            ReplaceJsonToken(json, search, replacement);

            // Return the modified JSON-formatted string
            return json.ToString();
        }

        /// <summary>
        /// Replaces a specified JSON token with a new token within a given token hierarchy.
        /// </summary>
        /// <param name="tokenToBeSearched">The token hierarchy to search.</param>
        /// <param name="searchToken">The token to be replaced.</param>
        /// <param name="replacementToken">The new token to use as a replacement.</param>
        public static void ReplaceJsonToken(JToken tokenToBeSearched, JToken searchToken, JToken replacementToken)
        {
            // Call the recursive token replacement method
            ReplaceToken(tokenToBeSearched, searchToken, replacementToken);
        }

        /// <summary>
        /// Recursive helper method to replace a specified JSON token with a new token within a given token hierarchy.
        /// </summary>
        /// <param name="token">The current token being processed.</param>
        /// <param name="searchToken">The token to be replaced.</param>
        /// <param name="replacementToken">The new token to use as a replacement.</param>
        /// <returns>True if the replacement is successful, false otherwise.</returns>
        private static bool ReplaceToken(JToken token, JToken searchToken, JToken replacementToken)
        {
            // Check if the current token is equal to the token to be replaced
            if (JToken.DeepEquals(token, searchToken))
            {
                // Replace the token if it matches the searchToken
                token.Replace(replacementToken);
                return true;
            }

            // Check if the token is an object
            if (token.Type == JTokenType.Object)
            {
                // Iterate through each property of the object
                foreach (JProperty child in token.Children<JProperty>())
                {
                    // Recursive call to search and replace deeper into the JSON structure
                    if (ReplaceToken(child.Value, searchToken, replacementToken))
                    {
                        return true;
                    }
                }
            }
            // Check if the token is an array
            else if (token.Type == JTokenType.Array)
            {
                // Iterate through each element of the array
                for (int i = 0; i < token.Children().Count(); i++)
                {
                    var tokenToSearch = token[i];
                    if(tokenToSearch == null) throw new ArgumentNullException(nameof(tokenToSearch));
                    // Recursive call to search and replace deeper into the JSON structure
                    if (ReplaceToken(tokenToSearch, searchToken, replacementToken))
                    {
                        return true;
                    }
                }
            }

            // Return false if the replacement is not successful at the current token level
            return false;
        }

        #endregion

        #endregion
    }
}
