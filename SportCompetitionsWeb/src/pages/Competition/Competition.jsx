import React, { useEffect, useState } from "react";
import classes from "./Competition.module.css";
import { useParams } from "react-router-dom";
import CompetitionApi from "../../api/competitionApi";
import useFetching from "../../hooks/useFetching";

const Competition = () => {
  const params = useParams();
  const [competition, competitionChange] = useState({});

  const getByIdCallback = async (id) => {
    const response = await CompetitionApi.getById(id);
    competitionChange(response.data);
  };

  const [fetchGetById] = useFetching(getByIdCallback);

  useEffect(() => {
    fetchGetById(params.id);
  }, []);

  return (
    <div>
      <div className={classes.logo}>Соревнование</div>
      <div className={classes.info}>
        <div className={classes.name}>{competition.name}</div>
        <div className={classes.params}>
          <div className={classes.date}>{competition.date}</div>
          <div>{competition.sport.name}</div>
        </div>
      </div>
    </div>
  );
};

export default Competition;
