import React, { useEffect, useState } from "react";
import useFetching from "./hooks/useFetching";
import HomeApi from "./api/homeApi";

const App = () => {
  const [hello, helloChange] = useState("");

  const helloCallback = async () => {
    const response = await HomeApi.get();
    helloChange(response.data);
  };

  const [fetchHello] = useFetching(helloCallback);

  useEffect(() => {
    fetchHello();
  }, []);

  return <div>{hello}</div>;
};

export default App;
