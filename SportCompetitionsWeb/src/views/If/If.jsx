import React from "react";

const If = ({ value, children }) => {
  if (value) return children;
  return <></>;
};

export default If;
