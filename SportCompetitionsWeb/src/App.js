import React, { useEffect, useState } from "react";
import useFetching from "./hooks/useFetching";
import HomeApi from "./api/homeApi";
import LeftMenu from "./layout/LeftMenu/LeftMenu";
import { BrowserRouter } from "react-router-dom";
import AppRouter from "./router/AppRouter/AppRouter";

const App = () => {
  // const [hello, helloChange] = useState("");

  // const helloCallback = async () => {
  //   const response = await HomeApi.get();
  //   helloChange(response.data);
  // };

  // const [fetchHello] = useFetching(helloCallback);

  // useEffect(() => {
  //   fetchHello();
  // }, []);

  return (
    <BrowserRouter>
      <AppRouter />
    </BrowserRouter>
  );
};

export default App;
