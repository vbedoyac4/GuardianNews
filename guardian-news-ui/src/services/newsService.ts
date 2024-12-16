import axios from "axios";
import { News } from "../models/News";

const API_BASE_URL = "https://localhost:7090/api";

export default {
  async getAllNews(): Promise<News[]> {
    try {
      const response = await axios.get(`${API_BASE_URL}/News`);
      return response.data;
    } catch (error) {
      console.error("Error al obtener todas las noticias:", error);
      return [];
    }
  },

  async getNews(params: { searchQuery: string }): Promise<News[]> {
    const { searchQuery } = params;
    const queryParams: Record<string, string> = {};

    if (searchQuery) {
      queryParams["searchInput"] = searchQuery;
    }
    try {
      const response = await axios.get(
        `${API_BASE_URL}/GuardianApi/searchcats`,
        {
          params: queryParams,
        }
      );

      return response.data;
    } catch (error) {
      console.error("Error fetching news:", error);
      return [];
    }
  },

  async addNews(news: News): Promise<void> {
    try {
      const response = await axios.post(`${API_BASE_URL}/News`, news);
      console.log("News added succesfully:", response.data);
    } catch (error) {
      console.error("Error adding  the news:", error);
      throw error;
    }
  },
};
