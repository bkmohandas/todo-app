import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5000/api", // adjust to backend port
  headers: { "Content-Type": "application/json" },
});
