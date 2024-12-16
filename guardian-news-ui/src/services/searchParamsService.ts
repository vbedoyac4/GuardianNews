import axios from "axios";
import { SearchParams } from "../models/SearchParams";

const API_BASE_URL = "https://localhost:7090/api";

export default {
  async getSearchParams(): Promise<SearchParams | null> {
    try {
      const response = await axios.get(`${API_BASE_URL}/SearchParam`);
      if (response.data && response.data.length > 0) {
        return response.data[0];
      }
      return null;
    } catch (error) {
      console.error("Error fetching SearchParams:", error);
      return null;
    }
  },

  async updateSearchParams(params: SearchParams): Promise<SearchParams> {
    try {
      const response = await axios.put(`${API_BASE_URL}/SearchParam`, params);
      return response.data;
    } catch (error) {
      console.error("Error updating SearchParams:", error);
      throw error;
    }
  },

  async addSearchParams(params: SearchParams): Promise<SearchParams> {
    try {
      const response = await axios.post(`${API_BASE_URL}/SearchParam`, params);
      return response.data;
    } catch (error) {
      console.error("Error adding SearchParams:", error);
      throw error;
    }
  },

  async deleteSearchParams(params: SearchParams): Promise<SearchParams> {
    try {
      const response = await axios.delete(
        `${API_BASE_URL}/SearchParam/${params.id}`
      );
      return response.data;
    } catch (error) {
      console.error("Error deleting SearchParams:", error);
      throw error;
    }
  },
};
