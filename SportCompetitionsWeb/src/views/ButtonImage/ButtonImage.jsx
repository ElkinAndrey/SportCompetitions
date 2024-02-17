import React from "react";
import classes from "./ButtonImage.module.css";

const ButtonImage = ({ src, title, onClick }) => {
  return (
    <button className={classes.button}>
      <img
        src={src}
        alt=""
        className={classes.image}
        title={title}
        onClick={onClick}
      />
    </button>
  );
};

export default ButtonImage;
