import axios from "axios";
import defaultURL from "./apiSettings";

const URL = `${defaultURL}/persons`;

class PersonApi {
  static async get() {
    const response = await axios.get(`${URL}`);
    return response;
  }

  static async getById(id) {
    const response = await axios.get(`${URL}/${id}`);
    return response;
  }

  static async getCompetitionsById(id) {
    const response = await axios.get(`${URL}/${id}/competition`);
    return response;
  }

  static async add(params) {
    await axios.post(`${URL}`, params);
  }

  static async delete(id) {
    await axios.delete(`${URL}/${id}`);
  }

  static async change(id, params) {
    await axios.put(`${URL}/${id}`, params);
  }
}

export default PersonApi;
