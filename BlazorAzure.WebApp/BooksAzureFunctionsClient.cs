﻿using System.Net.Http;
using System.Threading.Tasks;
using BlazorLibrary.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorAzure.WebApp
{
    public class BooksAzureFunctionsClient : IBooksClient
    {
        private readonly HttpClient _httpClient;
        private const string FunctionsHost = "<YOUR FUNCTION BASE URL HERE>";

        public string Token { get; set; }

        public BooksAzureFunctionsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteBook(Book book)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }
            await DeleteBook(book.Id);
        }

        public async Task DeleteBook(int id)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }

            var url = FunctionsHost + "/Books/Delete/" + id;

            await _httpClient.PostAsync(url, null);
        }

        public async Task<PagedResult<Book>> ListBooks(int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }

            var url = FunctionsHost + "/Books/Index/page/" + page;

            return await _httpClient.GetJsonAsync<PagedResult<Book>>(url);
        }

        public async Task<Book> GetBook(int id)
        {
            var url = FunctionsHost + "/Books/Get/" + id;

            return await _httpClient.GetJsonAsync<Book>(url);
        }

        public async Task SaveBook(Book book)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }

            var url = FunctionsHost + "/Books/Save";

            await _httpClient.PostJsonAsync<Book>(url, book);
        }

        public async Task<PagedResult<Book>> SearchBooks(string term, int page)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }

            var url = FunctionsHost + "/Books/Search/" + page + "/" + term;

            return await _httpClient.GetJsonAsync<PagedResult<Book>>(url);
        }
    }
}