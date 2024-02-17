import React from "react";
import classes from "./HeaderAdd.module.css";

const HeaderAdd = ({ text, onClick, className, style }) => {
  return (
    <div className={className} style={style}>
      <div className={classes.body}>
        <div className={classes.logo}>{text}</div>
        <button className={classes.plus} onClick={onClick}>
          +
        </button>
      </div>
    </div>
  );
};

export default HeaderAdd;
