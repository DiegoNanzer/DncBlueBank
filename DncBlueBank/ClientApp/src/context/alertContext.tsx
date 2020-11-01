import React, { createContext, Dispatch, ReactNode } from 'react'

interface IAlertContexData {
    message: string,
    visible: boolean,
    type: 'SUCCESS' | 'ERROR'
};

type Action =
     { typeAction: "HIDDEN",}
    |{ typeAction: "SHOW", message: string, type: 'SUCCESS' | 'ERROR' }


const AlertStateContext = createContext<IAlertContexData>({ message: "", visible: false, type: "SUCCESS" })

const AlertaDispatchContext = createContext<Dispatch<Action>>({} as Dispatch<Action>);

const reducer = (state: IAlertContexData, action: Action) => {

    console.log(state, action)
    switch (action.typeAction) {
        case "SHOW":
            return {
                ...state,
                message: action.message,
                type: action.type,
                visible: true
            };
        case "HIDDEN":
            return {
                ...state,
                message: state.message,
                visible: false
            };

        default:
            throw Error("action type não encontrado. ItensGridDispatchContext");
    }
}

interface IProps {
    children: ReactNode;
}


const AlerContextProvider = ({ children }: IProps) => {
    const [state, dispatch] = React.useReducer(reducer, {  message: "", visible: false, type: "SUCCESS" }, undefined);

    return (
        <AlertStateContext.Provider value={state}>
            <AlertaDispatchContext.Provider value={dispatch}>
                {children}
            </AlertaDispatchContext.Provider>
        </AlertStateContext.Provider>
    )
}

export { AlerContextProvider, AlertaDispatchContext, AlertStateContext }