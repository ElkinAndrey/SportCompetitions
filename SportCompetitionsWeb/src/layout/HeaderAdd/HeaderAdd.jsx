import React from "react";
import classes from "./HeaderAdd.module.css";

const HeaderAdd = ({
  text,
  onClick,
  className,
  style,
  fontSize = "24px",
  title = "",
}) => {
  return (
    <div className={className} style={style}>
      <div className={classes.body}>
        <div className={classes.logo} style={{ fontSize: fontSize }}>
          {text}
        </div>
        <button className={classes.plus} onClick={onClick} title={title}>
          +
        </button>
      </div>
    </div>
  );
};

export default HeaderAdd;
