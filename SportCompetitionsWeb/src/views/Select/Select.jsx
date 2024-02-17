import React from "react";
import classes from "./Select.module.css";

const Select = ({
  value,
  onChange,
  options,
  startName,
  margin = "0px",
  placeholder = "",
}) => {
  function selectToggle() {
    document
      .getElementsByClassName(classes.select)
      .item(0)
      .classList.toggle(classes.isActive);
  }

  function selectChoose(e, value) {
    onChange(value);
    let text = e.target.innerText;
    document.getElementsByClassName(classes.select__current).item(0).innerText =
      text;
    document
      .getElementsByClassName(classes.select)
      .item(0)
      .classList.remove(classes.isActive);
  }

  document.addEventListener("click", (e) => {
    const div = document.getElementsByClassName(classes.select).item(0);
    if (div === null) {
      return;
    }
    const withinBoundaries = e.composedPath().includes(div);
    let selectHeader = document
      .getElementsByClassName(classes.select__header)
      .item(0);
    let label = document.getElementsByClassName(classes.select__label).item(0);

    if (withinBoundaries) {
      if (!selectHeader.classList.contains(classes.header__active)) {
        selectHeader.classList.toggle(classes.header__active);
      }
      if (!label.classList.contains(classes.select__label__active)) {
        label.classList.toggle(classes.select__label__active);
      }
    } else {
      selectHeader.classList.remove(classes.header__active);
      label.classList.remove(classes.select__label__active);
      div.classList.remove(classes.isActive);
    }
  });

  return (
    <div>
      <div className={classes.select} style={{ margin: margin }}>
        <div className={classes.select__label}>{placeholder}</div>
        <div className={classes.select__header} onClick={selectToggle}>
          <span className={classes.select__current}>{startName}</span>
          <div className={classes.select__icon}>&#9699;</div>
        </div>

        <div className={classes.select__body}>
          {options.map((opt, index) => (
            <div key={opt.value}>
              {index !== 0 ? <div className={classes.line}></div> : <div></div>}
              <div
                className={classes.select__item}
                onClick={(e) => selectChoose(e, opt.value)}
                style={{ background: opt.value === value ? "#e1e1e1" : "" }}
              >
                {opt.name}
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Select;
