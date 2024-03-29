import React from "react";
import classes from "./LeftMenu.module.css";
import MenuLink from "../../views/MenuLink/MenuLink";

const LeftMenu = ({ children }) => {
  return (
    <div className={classes.body}>
      <div className={classes.bodyMenu}>
        <div className={classes.logo}>Меню</div>
        <div className={classes.links}>
          <MenuLink src="/images/competitions.png" text="Соревнования" to="/" />
          <MenuLink src="/images/persons.png" text="Люди" to="/persons" />
          <MenuLink src="/images/sports.png" text="Виды спорта" to="/sports" />
        </div>
      </div>
      <div className={classes.children}>{children}</div>
    </div>
  );
};

export default LeftMenu;
