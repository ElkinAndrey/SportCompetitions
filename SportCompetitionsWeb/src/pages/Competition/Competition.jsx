import React, { useEffect, useState } from "react";
import classes from "./Competition.module.css";
import { useParams } from "react-router-dom";
import CompetitionApi from "../../api/competitionApi";
import useFetching from "../../hooks/useFetching";
import ButtonLink from "../../views/ButtonLink/ButtonLink";
import ButtonDelete from "../../views/ButtonDelete/ButtonDelete";
import BigLoader from "../../views/BigLoader/BigLoader";
import ButtonImage from "../../views/ButtonImage/ButtonImage";
import Modal from "../../views/Modal/Modal";
import Input from "../../views/Input/Input";
import Button from "../../views/Button/Button";
import Select from "../../views/Select/Select";
import SportApi from "../../api/sportApi";

const Competition = () => {
  const params = useParams();

  const [isOpenModal, isOpenModalChange] = useState(false);
  const [competition, competitionChange] = useState({});
  const [persons, personsChange] = useState([]);
  const [personsNot, personsNotChange] = useState([]);
  const [deletedId, deletedIdChange] = useState(null);
  const [addId, addIdChange] = useState(null);
  const [sports, sportsChange] = useState([]);
  const [newName, newCompetitionsNameChange] = useState("");
  const [newDate, newDateChange] = useState("");
  const [newSport, newSportChange] = useState(null);

  const getByIdCallback = async (id) => {
    const response = await CompetitionApi.getById(id);
    competitionChange(response.data);
    newCompetitionsNameChange(response.data.name);
    newDateChange(response.data.date);
    newSportChange(response.data.sport.id);
  };

  const getPersonsCallback = async (id) => {
    const response = await CompetitionApi.getUsers(id);
    personsChange(response.data);
  };

  const getNotParticipatingPersonsCallback = async (id) => {
    const response = await CompetitionApi.getNotParticipatingUsers(id);
    personsNotChange(response.data);
  };

  const deleteFromCompetitionCallback = async (personId) => {
    await CompetitionApi.deleteFromCompetition(params.id, personId);
    let deletedPerson = persons[persons.findIndex((p) => p.id === personId)];
    personsNotChange([deletedPerson, ...personsNot]);
    const newPersons = persons.filter(
      (competition) => competition.id !== personId
    );
    personsChange([...newPersons]);
    deletedIdChange(null);
  };

  const addToCompetitionCallback = async (personId) => {
    await CompetitionApi.addToCompetition(params.id, personId);
    let addedPerson =
      personsNot[personsNot.findIndex((p) => p.id === personId)];
    personsChange([addedPerson, ...persons]);
    const newPersonsNot = personsNot.filter(
      (competition) => competition.id !== personId
    );
    personsNotChange([...newPersonsNot]);
    addIdChange(null);
  };

  const getSportsCallback = async () => {
    const response = await SportApi.get();
    const sports = response.data.map((sport) => {
      return { value: sport.id, name: sport.name };
    });
    sportsChange(sports);
  };

  const changeCallback = async () => {
    const p = {
      name: newName,
      date: newDate,
      sportId: newSport,
    };
    await CompetitionApi.change(params.id, p);
    isOpenModalChange(false);
    const sport = sports[sports.findIndex((p) => p.value === newSport)];
    const newCompetition = {
      name: newName,
      date: newDate,
      sport: { id: sport.value, name: sport.name },
    };
    competitionChange(newCompetition);
  };

  const [fetchGetById, isLoadingGetById] = useFetching(getByIdCallback);
  const [fetchGetUsers, isLoadingGetUsers] = useFetching(getPersonsCallback);
  const [fetchGetNotParticipatingUsers, isLoadingGetNotParticipatingUsers] =
    useFetching(getNotParticipatingPersonsCallback);
  const [fetchDeleteFromCompetition, isLoadingDeleteFromCompetition] =
    useFetching(deleteFromCompetitionCallback);
  const [fetchAddToCompetition, isLoadingAddToCompetition] = useFetching(
    addToCompetitionCallback
  );
  const [fetchGetSports, isLoadingfetchGetSports] =
    useFetching(getSportsCallback);
  const [fetchChange, isLoadingChange] = useFetching(changeCallback);

  const del = (id) => {
    if (isLoadingDeleteFromCompetition || isLoadingAddToCompetition) return;
    deletedIdChange(id);
    fetchDeleteFromCompetition(id);
  };

  const add = (id) => {
    if (isLoadingDeleteFromCompetition || isLoadingAddToCompetition) return;
    addIdChange(id);
    fetchAddToCompetition(id);
  };

  const change = () => {
    isOpenModalChange(true);
  };

  useEffect(() => {
    fetchGetById(params.id);
    fetchGetUsers(params.id);
    fetchGetNotParticipatingUsers(params.id);
    fetchGetSports();
  }, []);

  const Head = () => (
    <div className={classes.header}>
      <div className={classes.logo}>Соревнование</div>
      <ButtonImage
        src="/images/change.png"
        title="Изменить соревнование"
        onClick={change}
      />
    </div>
  );

  if (
    isLoadingGetById ||
    isLoadingGetUsers ||
    isLoadingGetNotParticipatingUsers ||
    isLoadingfetchGetSports
  )
    return (
      <div>
        <Head />
        <BigLoader />
      </div>
    );

  return (
    <div>
      <Head />
      <div className={classes.info}>
        <div className={classes.name}>{competition.name}</div>
        <div className={classes.params}>
          <div className={classes.date}>{competition.date}</div>
          <div>{competition.sport?.name}</div>
        </div>
      </div>
      <div className={classes.minlogo}>Участники</div>
      <table className={classes.participants}>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Email</th>
            <th>День рождения</th>
            <th>Открыть</th>
            <th>Исключить</th>
          </tr>
        </thead>
        <tbody>
          {persons.map((person, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{person.name}</td>
              <td className={classes.tableDate}>{person.email}</td>
              <td className={classes.tableSport}>{person.dateOfBirth}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/persons/${person.id}`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Исключить"
                  onClick={() => del(person.id)}
                  isLoading={
                    isLoadingDeleteFromCompetition && person.id === deletedId
                  }
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className={classes.minlogo}>Остальные люди</div>
      <table>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Email</th>
            <th>День рождения</th>
            <th>Открыть</th>
            <th>Исключить</th>
          </tr>
        </thead>
        <tbody>
          {personsNot.map((person, index) => (
            <tr key={index}>
              <td className={classes.tableName}>{person.name}</td>
              <td className={classes.tableDate}>{person.email}</td>
              <td className={classes.tableSport}>{person.dateOfBirth}</td>
              <td className={classes.tableButton}>
                <ButtonLink text="Открыть" to={`/persons/${person.id}`} />
              </td>
              <td className={classes.tableButton}>
                <ButtonDelete
                  text="Добавить"
                  onClick={() => add(person.id)}
                  isLoading={isLoadingAddToCompetition && person.id === addId}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <Modal active={isOpenModal} setActive={isOpenModalChange}>
        <div className={classes.modalBody}>
          <div className={classes.modalLogo}>Изменить соревнование</div>
          <Input
            value={newName}
            setValue={newCompetitionsNameChange}
            placeholder="Название"
            className={classes.inputName}
          />
          <input
            type="datetime-local"
            value={newDate}
            onChange={(e) => newDateChange(e.target.value)}
            className={classes.inputDate}
          />
          <Select
            value={newSport}
            onChange={newSportChange}
            options={sports}
            startName={"Спорт"}
            placeholder={"Вид спорта"}
          />
          <Button
            text={"Сохранить"}
            className={classes.addButton}
            isLoading={isLoadingChange}
            onClick={fetchChange}
          />
        </div>
      </Modal>
    </div>
  );
};

export default Competition;
