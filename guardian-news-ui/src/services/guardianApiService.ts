import axios from "axios";
import { News } from "../models/News";

const API_URL = "https://localhost:7090/api/GuardianApi/";

export default {
  async fetchNewsBySearchInput(params: {
    searchQuery: string;
  }): Promise<News[]> {
    const { searchQuery } = params;
    try {
      const response = await axios.get(`${API_URL}search${searchQuery}`);
      return response.data;
    } catch (error) {
      console.error("Error fetching news by search input:", error);
      throw error;
    }
  },

  async fetchNewsFromGuardianApi() {
    try {
      const response = await axios.get(`${API_URL}news`);
      return response.data;
    } catch (error) {
      console.error("Error fetching news from Guardian API:", error);
      throw error;
    }
  },
};
