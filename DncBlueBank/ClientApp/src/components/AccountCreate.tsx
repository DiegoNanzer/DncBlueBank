import React, { useContext } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, Alert } from 'reactstrap';
import { Link, Redirect, useHistory } from 'react-router-dom';
import { useForm } from "react-hook-form";
import { IAccountModel } from '../models/Account';
import { API_ACCOUNT } from '../constants';
import { AlertaDispatchContext } from '../context/alertContext';
import { IErrorMessage } from '../models/ErrorMessage';
import './container.css';

export const AccountCreate: React.FC = () => {
    const { register, handleSubmit, errors } = useForm<IAccountModel>();
    const AlertDispatch = useContext(AlertaDispatchContext);
    const history = useHistory();

    const onSubmit = handleSubmit(async (data) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        };

        const response = await fetch(API_ACCOUNT, requestOptions);

        if (response.status == 201) {
            AlertDispatch({ typeAction: "SHOW", type: "SUCCESS", message: "Account created" });

            setTimeout(function () {
                AlertDispatch({ typeAction: "HIDDEN" });
                history.push("/account");
            }, 3000)

        }
        else if (response.status == 400) {
            const data = await response.json();

            AlertDispatch({ typeAction: "SHOW", type: "ERROR", message: (data as IErrorMessage).message });
        }
        else {
            console.log(response);
        }
    });


    return <div className="container myContainer">
        <h1>Account Form</ h1>
        <form onSubmit={onSubmit}>
            <div className="form-group">
                <label htmlFor="agency">Agency</label>
                <input type="text" ref={register({ required: true, maxLength: 10 })} name="agency" id="agency" placeholder="Agency" className="form-control" />
                {errors.agency && errors.agency.type == "required" && <Alert color="warning">Agency is required</Alert>}
                {errors.agency && errors.agency.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
            </div>
            <div className="form-group">
                <label htmlFor="number">Account Number</label>
                <input type="text" ref={register({ required: true, maxLength: 10 })} name="number" id="number" placeholder="Account Number" className="form-control" />
                {errors.number && errors.number.type == "required" && <Alert color="warning">Number is required</Alert>}
                {errors.number && errors.number.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
            </div>
            <div className="form-group">
                <label htmlFor="owner">Owner</label>
                <input type="text" ref={register({ required: true, maxLength: 60 })} name="owner" id="owner" placeholder="Owner" className="form-control" />
                {errors.owner && errors.owner.type == "required" && <Alert color="warning">Owner is required</Alert>}
                {errors.owner && errors.owner.type == "maxLength" && <Alert color="warning">MaxLength is 10</Alert>}
            </div>
            <button className="btn btn-primary">Save</button>{' '}
            <Link className="btn btn-danger" to="/account">Cancel</Link>
        </form>
    </div>
}