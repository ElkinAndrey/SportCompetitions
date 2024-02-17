import axios from "axios";
import defaultURL from "./apiSettings";

const URL = `${defaultURL}/sports`;

class SportApi {
  static async get() {
    const response = await axios.get(`${URL}`);
    return response;
  }
}

export default SportApi;
