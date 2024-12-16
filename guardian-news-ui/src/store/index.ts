import { createStore } from "vuex";
import { SearchParams } from "../models/SearchParams";
import { News } from "../models/News";
import searchParamsService from "../services/searchParamsService";
import newsService from "../services/newsService";
import guardianApiService from "../services/guardianApiService"; // Import the new service

interface State {
  searchParams: SearchParams[]; // Lista de parámetros de búsqueda
  news: News[]; // Lista de noticias
}

export const store = createStore<State>({
  state: {
    searchParams: [], // Lista de parámetros de búsqueda
    news: [], // Lista de noticias
  },
  mutations: {
    setSearchParams(state, params: SearchParams[]) {
      state.searchParams = params;
    },
    setNews(state, news: News[]) {
      state.news = news;
    },
    addSearchParam(state, searchParam: SearchParams) {
      state.searchParams.push(searchParam); // Add the new search param to the state
    },
    addNews(state, newNews: News[]) {
      state.news = [...state.news, ...newNews]; // Add new news to the existing news
    },
  },
  actions: {
    async loadSearchParams({ commit }) {
      // Cargar parámetros de búsqueda desde la API
      const savedParams = await searchParamsService.getSearchParams();
      commit("setSearchParams", savedParams);
    },

    // Acción para obtener noticias con parámetros
    async fetchNews({ commit }, searchQuery: string) {
      // Si existe un parámetro válido, obtenemos noticias filtradas
      if (searchQuery) {
        const fetchedNews = await newsService.getNews({ searchQuery });
        commit("addNews", fetchedNews); // Add the new news to the existing news

        // Add search query to the searchParams list or database
        const newSearchParam: SearchParams = {
          id: 0,
          type: "search",
          value: searchQuery,
        }; // Assuming `SearchParams` has a `query` property
        commit("addSearchParam", newSearchParam);

        // Optionally, save the new search parameter to the database using the service
        await searchParamsService.addSearchParams(newSearchParam); // Change made here to plural
      }
    },

    // Acción para obtener todas las noticias sin parámetros
    async getAllNews({ commit }) {
      const fetchedNews = await newsService.getAllNews();
      commit("setNews", fetchedNews);
    },

    // Acción para agregar parámetros de búsqueda
    async addSearchParams({ commit }, searchQuery: string) {
      const newSearchParam: SearchParams = {
        id: 0,
        type: "search",
        value: searchQuery,
      };
      commit("addSearchParam", newSearchParam);
      await searchParamsService.addSearchParams(newSearchParam);
    },

    // New action to fetch news by search input using the new service
    async fetchNewsBySearchInput(
      { commit },
      searchQuery: string
    ): Promise<News[]> {
      if (searchQuery) {
        const fetchedNews = await guardianApiService.fetchNewsBySearchInput({
          searchQuery,
        });
        return fetchedNews; // Return the fetched news
      }
      return []; // Return an empty array if no search query
    },

    // New action to fetch all news from Guardian API using the new service
    async fetchNewsFromGuardianApi({ commit }) {
      const fetchedNews = await guardianApiService.fetchNewsFromGuardianApi();
      commit("setNews", fetchedNews);
    },

    // New action to add news from Guardian API using the new service
    async addNewsFromGuardianApi({ commit }, newNews: News[]) {
      commit("addNews", newNews);
    },

    // New action to add a single news item to the database
    async addNewsToDb({ commit }, newNews: News) {
      await newsService.addNews(newNews);
      commit("addNews", [newNews]);
    },
  },
});
