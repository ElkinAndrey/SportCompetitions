import React from "react";
import classes from "./ButtonLink.module.css";
import If from "../If/If";
import Loader from "../Loader/Loader";
import { Link } from "react-router-dom";

const ButtonLink = ({
  text,
  className,
  style,
  onClick,
  isLoading,
  to = "/",
}) => {
  return (
    <div className={className} style={style}>
      <Link className={classes.button} onClick={onClick} to={to}>
        <If value={isLoading}>
          <Loader color="#000000" className={classes.loader} />
        </If>
        <div className={isLoading ? classes.textHidden : classes.text}>
          {text}
        </div>
      </Link>
    </div>
  );
};

export default ButtonLink;
