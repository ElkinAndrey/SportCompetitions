import React from "react";
import classes from "./ButtonDelete.module.css";
import If from "../If/If";
import Loader from "../Loader/Loader";

const ButtonDelete = ({ text, onClick, isLoading }) => {
  return (
    <button className={classes.button} onClick={onClick}>
      <If value={isLoading}>
        <Loader color="#000000" className={classes.loader} />
      </If>
      <div className={isLoading ? classes.textHidden : classes.text}>
        {text}
      </div>
    </button>
  );
};

export default ButtonDelete;
