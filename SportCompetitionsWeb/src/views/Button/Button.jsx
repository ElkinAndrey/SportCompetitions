import React from "react";
import classes from "./Button.module.css";
import If from "../If/If";
import Loader from "../Loader/Loader";

const Button = ({ text, className, style, onClick, isLoading }) => {
  return (
    <div className={className} style={style}>
      <button className={classes.button} onClick={onClick}>
        <If value={isLoading}>
          <Loader color="#000000" className={classes.loader} />
        </If>
        <div className={isLoading ? classes.textHidden : classes.text}>
          {text}
        </div>
      </button>
    </div>
  );
};

export default Button;
