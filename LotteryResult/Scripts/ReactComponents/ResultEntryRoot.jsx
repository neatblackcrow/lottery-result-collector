class ResultEntryRoot extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            currentState: "preload"
        };

        this.goBackToPreloadState = this.goBackToPreloadState.bind(this);
        this.goToResultInsertState = this.goToResultInsertState.bind(this);
    }

    goBackToPreloadState() {
        this.setState({
            currentState: "preload"
        });
    }

    goToResultInsertState() {
        this.setState({
            currentState: "insert"
        });
    }

    render() {
        const isPreload = (this.state.currentState == "preload") ? true : false;
        return (
            isPreload ? (
                <ResultEntryPreload toggleState={this.goToResultInsertState} />
            ) : (
                <ResultEntryInsert toggleState={this.goBackToPreloadState} />
                )
            );
    }
}

ReactDOM.render(
    <ResultEntryRoot />,
    document.getElementById('ResultEntryRoot')
);