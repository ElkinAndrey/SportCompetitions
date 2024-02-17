import axios from "axios";
import defaultURL from "./apiSettings";

const URL = `${defaultURL}/competitions`;

class CompetitionApi {
  static async get() {
    const response = await axios.get(`${URL}`);
    return response;
  }

  static async getById(id) {
    const response = await axios.get(`${URL}/${id}`);
    return response;
  }

  static async add(params) {
    await axios.post(`${URL}`, params);
  }

  static async delete(id) {
    await axios.delete(`${URL}/${id}`);
  }
}

export default CompetitionApi;
