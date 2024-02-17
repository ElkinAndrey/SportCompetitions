import React from "react";
import classes from "./MenuLink.module.css";
import If from "../If/If";
import { Link } from "react-router-dom";

const MenuLink = ({ src = null, text = "", to = "/", className, style }) => {
  return (
    <div className={className} style={style}>
      <Link className={classes.body} to={to}>
        <If value={src !== null}>
          <img src={src} alt="" className={classes.image} draggable="false" />{" "}
        </If>
        <div className={classes.text}>{text}</div>
      </Link>
    </div>
  );
};

export default MenuLink;
