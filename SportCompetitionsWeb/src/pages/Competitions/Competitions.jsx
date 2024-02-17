import React, { useEffect, useState } from "react";
import classes from "./Competitions.module.css";
import CompetitionApi from "../../api/competitionApi";
import useFetching from "../../hooks/useFetching";
import ButtonDelete from "../../views/ButtonDelete/ButtonDelete";
import Button from "../../views/Button/Button";
import HeaderAdd from "../../layout/HeaderAdd/HeaderAdd";
import Modal from "../../views/Modal/Modal";
import Input from "../../views/Input/Input";
import Select from "../../views/Select/Select";
import SportApi from "../../api/sportApi";
import ButtonLink from "../../views/ButtonLink/ButtonLink";

const Competitions = () => {
  const [isOpenModal, isOpenModalChange] = useState(false);
  const [deletedId, deletedIdChange] = useState(null);
  const [competitions, competitionsChange] = useState([]);
  const [sports, sportsChange] = useState([]);
  const [newCompetitionsName, newCompetitionsNameChange] = useState("");
  const [newCompetitionsDate, newCompetitionsDateChange] = useState("");
  const [newCompetitionsSport, newCompetitionsSportChange] = useState(null);

  const getCallback = async () => {
    const response = await CompetitionApi.get();
    competitionsChange(response.data);
  };

  const getSportsCallback = async () => {
    const response = await SportApi.get();
    const sports = response.data.map((sport) => {
      return { value: sport.id, name: sport.name };
    });
    sportsChange(sports);
  };

  const deleteCallback = async (id) => {
    await CompetitionApi.delete(id);
    const newCompetitions = competitions.filter(
      (competition) => competition.id !== id
    );
    competitionsChange([...newCompetitions]);
    deletedIdChange(null);
  };

  const addCallback = async () => {
    const params = {
      name: newCompetitionsName,
      date: newCompetitionsDate,
      sportId: newCompetitionsSport,
    };
    await CompetitionApi.add(params);
    isOpenModalChange(false);
    newCompetitionsNameChange("");
    newCompetitionsDateChange("");
    newCompetitionsSportChange("");
  };

  const [fetchGet] = useFetching(getCallback);
  const [fetchGetSports] = useFetching(getSportsCallback);
  const [fetchDelete, isLoadingDelete] = useFetching(deleteCallback);
  const [fetchAdd, isLoadingAdd] = useFetching(addCallback);

  const del = (id) => {
    if (isLoadingDelete) return;
    deletedIdChange(id);
    fetchDelete(id);
  };

  const openModal = () => {
    isOpenModalChange(true);
  };

  useEffect(() => {
    fetchGet();
    fetchGetSports();
  }, []);

  return (
    <div>
      <HeaderAdd
        text="Соревнования"
        className={classes.header}
        onClick={openModal}
      />
      <table>
        <thead>
          <tr>
            <th>Название</th>
            <th>Дата</th>
            <th>Вид спорта</th>
            <th>Открыть</th>
            <th>Изменить</th>
            <th>Удалить</th>
          </tr>
        </thead>
        <tbody>
          {competitions.map((competition, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{competition.name}</td>
              <td className={classes.tableDate}>{competition.date}</td>
              <td className={classes.tableSport}>{competition.sport.name}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/${competition.id}`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonLink text="Изменить" to={`/${competition.id}/change`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Удалить"
                  onClick={() => del(competition.id)}
                  isLoading={isLoadingDelete && competition.id === deletedId}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Modal active={isOpenModal} setActive={isOpenModalChange}>
        <div className={classes.modalBody}>
          <div className={classes.modalLogo}>Добавить соревнование</div>
          <Input
            value={newCompetitionsName}
            setValue={newCompetitionsNameChange}
            placeholder="Название"
            className={classes.inputName}
          />
          <input
            type="datetime-local"
            value={newCompetitionsDate}
            onChange={(e) => newCompetitionsDateChange(e.target.value)}
            className={classes.inputDate}
          />
          <Select
            value={newCompetitionsSport}
            onChange={newCompetitionsSportChange}
            options={sports}
            startName={"Спорт"}
            placeholder={"Вид спорта"}
          />
          <Button
            text={"Добавить"}
            className={classes.addButton}
            isLoading={isLoadingAdd}
            onClick={fetchAdd}
          />
        </div>
      </Modal>
    </div>
  );
};

export default Competitions;
