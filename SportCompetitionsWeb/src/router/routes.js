import { Navigate } from "react-router-dom";
import Competitions from "../pages/Competitions/Competitions";
import Persons from "../pages/Persons/Persons";
import Sports from "../pages/Sports/Sports";

const routes = [
  { path: "/", element: <Competitions />, exact: true },
  { path: "*", element: <Navigate to="/" />, exact: true },
  { path: "persons", element: <Persons />, exact: true },
  { path: "sports", element: <Sports />, exact: true },
];

export default routes;
