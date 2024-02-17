import React from "react";
import { Route, Routes } from "react-router-dom";
import routes from "../routes";
import LeftMenu from "../../layout/LeftMenu/LeftMenu";

const AppRouter = () => {
  return (
    <LeftMenu>
      <Routes path="/">
        {routes.map((r, index) => (
          <Route
            key={index}
            exact={r.exact}
            path={r.path}
            element={r.element}
          ></Route>
        ))}
      </Routes>
    </LeftMenu>
  );
};

export default AppRouter;
