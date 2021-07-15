import React, {useState} from 'react';
import Header from './Components/Header/Header';
import Container from './Components/Container/Container';


import './App.css';


function App() {

  return (
    <div className="App">
      <div>
        <Header/>
      </div>
      <div>
        <Container/>
      </div>
    </div>
  );
}

export default App;
