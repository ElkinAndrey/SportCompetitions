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

  static async getUsers(id) {
    const response = await axios.get(`${URL}/${id}/persons`);
    return response;
  }

  static async getNotParticipatingUsers(id) {
    const response = await axios.get(`${URL}/${id}/persons/not`);
    return response;
  }

  static async deleteFromCompetition(competitionId, personId) {
    const response = await axios.put(`${URL}/${competitionId}/person/delete`, {
      personId: personId,
    });
    return response;
  }

  static async addToCompetition(competitionId, personId) {
    const response = await axios.put(`${URL}/${competitionId}/person/add`, {
      personId: personId,
    });
    return response;
  }
}

export default CompetitionApi;
