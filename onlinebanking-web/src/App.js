import React from 'react';
import {
  BrowserRouter as Router,
  Route,
  Switch
} from 'react-router-dom';
import HomeScreen from './screens/Home/Home';

export default () => (
  <Router>
    <Switch>
      <Route exact path="/" component={HomeScreen} />
    </Switch>
  </Router>
)
